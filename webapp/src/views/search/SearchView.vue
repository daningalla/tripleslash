<script setup lang="ts">
import vFocus from "@/directives/auto-focus";
import ToggleWidget from "@/widgets/ToggleWidget.vue";
import SearchResults from "@/views/search/SearchResults.vue";
import { reactive, watch } from "vue";
import { debounceTime, Observable, switchMap } from "rxjs";
import { resourceService } from "@/services/resource-service";
import type { SearchResult } from "@/dto/search";
import type { RestApiResponse } from "@/dto/rest-api-response";

interface MyState {
  term: string;
  prerelease: boolean;
}

const state = reactive<MyState>({
  term: "",
  prerelease: true,
});

const resultState = reactive<{ target?: SearchResult }>({ target: undefined });

const watchObservable = new Observable<MyState>((sub) => {
  watch(state, (value) => sub.next(value));
});

watchObservable
  .pipe(
    debounceTime(250),
    switchMap((value: MyState) => {
      return resourceService.search(value.term, 0, 10, value.prerelease);
    })
  )
  .subscribe((response: RestApiResponse<SearchResult>) => {
    console.log("Set result", response);
    resultState.target = response.result;
  });
</script>

<template>
  <div class="flex-down align-items-center">
    <div class="flex-down content-wrap">
      <div class="input-wrap flex-across justify-space-between">
        <input
          v-focus
          v-model="state.term"
          class="input font-lg"
          type="text"
          placeholder="Search package, type, etc..."
        />
        <ToggleWidget
          class="font-reg"
          label="Include prerelease"
          v-model="state.prerelease"
        />
      </div>
    </div>
    <SearchResults
      class="results"
      v-if="resultState.target?.groups"
      :results="resultState.target"
    />
  </div>
</template>

<style scoped>
.content-wrap {
  max-width: 800px;
  width: 100%;
  margin-top: 32px;
}
.input-wrap {
  background-color: var(--ts-background-header-search);
  border-radius: 5px;
  padding: 16px;
}
.input {
  flex-grow: 1;
  border: none;
  background-color: transparent;
  color: var(--ts-foreground-search-input);
  outline: none;
}
.results {
  margin-top: 32px;
}
</style>
