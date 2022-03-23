<script setup>
import { reactive, watch } from "vue";
import vFocus from "../directives/auto-focus.js";
import ToggleWidget from "../widgets/ToggleWidget.vue";
import { debounceTime } from "rxjs/operators";
import { Observable } from "rxjs";
import searchService from "../services/search-service-api";
import serviceIndex from "../services/service-index-api";

const state = reactive({
    prerelease: true,
    term: "",
});

const watchObservable = new Observable(sub => {
    watch(state, value => {
        sub.next(value);
    });
}).pipe(debounceTime(500));

watchObservable.subscribe({
    next(value) {
        searchService
            .getResource(state.term, 0, 10, state.prerelease)
            .subscribe((result) => {
                console.log("Search result", result);
            });
    }
});

</script>

<template>
    <div class="flex justify-content-center">
        <div class="flex inner flex-grow">
            <input type="text"
                    v-focus
                    v-model="state.term"
                    placeholder="Package, class, member, ..."                    
                    />            
            <ToggleWidget label="Include prerelease" v-model="state.prerelease" />
        </div>
    </div>
</template>

<style scoped>
.inner {
    background-color: var(--theme-background-search);
    border-radius: var(--border-radius-lg);
    max-width: 800px;
    margin-top: 32px;
    padding: 16px;
}
input {
    background-color: transparent;
    border: none;
    outline: none;
    color: var(--theme-search-text);
    font-size: var(--font-sz-lg);
    width: 100%;    
}
</style>