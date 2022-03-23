<script setup lang="ts">
import type { TabItem } from "@/core/tab-types";

const props = defineProps<{ item: TabItem; selected: boolean }>();
const emit = defineEmits(["selected"]);
const handleClicked = () => {
  emit("selected", props.item);
};
</script>

<template>
  <div class="component flex-down cursor-pointer" @click="handleClicked">
    <div class="label">{{ item.label }}</div>
    <div :class="{ selected: selected, 'lower-ribbon': true }"></div>
  </div>
</template>

<style scoped>
.component {
  margin-right: 16px;
}
.label {
  margin-bottom: 5px;
}
.lower-ribbon {
  height: 4px;
}
.selected {
  background-color: var(--ts-widget-highlight);
}
</style>
