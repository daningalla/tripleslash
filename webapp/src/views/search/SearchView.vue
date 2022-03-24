<script setup lang="ts">
import type { SearchResult } from "@/dto/search";
import type { RestApiResponse } from "@/dto/rest-api-response";
import { reactive, watch, ref, computed } from "vue";
import { debounceTime, Observable, switchMap } from "rxjs";
import { resourceService } from "@/services/resource-service";
import { ofEmpty } from "@/dto/rest-api-response";
import vFocus from "@/directives/auto-focus";
import ToggleWidget from "@/widgets/ToggleWidget.vue";
import SearchResults from "@/views/search/SearchResults.vue";
import PagerWidget from "@/widgets/PagerWidget.vue";

interface MyState {
  term: string;
  prerelease: boolean;
  page: number;
}

const state = reactive<MyState>({
  term: "",
  prerelease: true,
  page: 0,
});

const resultState = reactive<{ target?: SearchResult }>({ target: undefined });
const rerender = ref(0);
const watchObservable = new Observable<MyState>((sub) => {
  watch(state, (value) => sub.next(value));
});
const hasNext = computed(() => {
  const groups = resultState.target?.groups;
  return groups && groups.findIndex((group) => group.hasNextPage) > -1;
});
watchObservable
  .pipe(
    debounceTime(250),
    switchMap((value: MyState) => {
      return value.term.length >= 4
        ? resourceService.search(value.term, state.page, 20, value.prerelease)
        : ofEmpty<SearchResult>();
    })
  )
  .subscribe((response: RestApiResponse<SearchResult>) => {
    resultState.target = response.result;
    rerender.value++;
  });
</script>

<template>
  <div class="flex-down align-items-center search-view">
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
      class="flex-down results"
      v-if="resultState.target?.groups"
      :results="resultState.target"
      :key="rerender"
    />
    <PagerWidget :page="state.page" :hasNext="hasNext ?? false" @nextClicked="state.page++" />
  </div>
</template>

<style scoped>
.search-view {
  padding: 0 16px;
}
.content-wrap {
  max-width: var(--ts-search-width);
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
  max-width: var(--ts-search-width);
  width: 100%;
}
.more {
}
</style>
