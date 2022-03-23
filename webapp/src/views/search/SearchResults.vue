<script setup lang="ts">
import type { SearchResult } from "@/dto/search";
import { computed, reactive } from "vue";
import { TabItem } from "@/core/tab-types";
import TabHeaderWidget from "@/widgets/TabHeaderWidget.vue";

const props = defineProps<{ results: SearchResult }>();
const tabItems = computed(() => {
  const items = props.results.groups.map((group) => {
    return new TabItem(group.providerKey);
  });
  return items ?? [];
});
const group = reactive({ value: props.results.groups[0] });
const handleSelectionChanged = (item: TabItem) => {
  group.value = props.results.groups.find(
    (group) => group.providerKey == item.key
  )!;
};
</script>

<template>
  <div>
    <TabHeaderWidget
      :items="tabItems"
      :selectionChanged="handleSelectionChanged"
    />
  </div>
</template>

<style scoped></style>
