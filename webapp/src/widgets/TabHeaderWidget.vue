<script setup lang="ts">
import type { TabItem } from "@/core/tab-types";
import TabItemWidget from "@/widgets/TabItemWidget.vue";
import { ref } from "vue";

const props = defineProps<{ items: TabItem[] }>();
const selected = ref(0);
const emit = defineEmits(["selectionChanged"]);
const handleSelected = (item: TabItem) => {
  selected.value = props.items.indexOf(item);
  emit("selectionChanged", item.key);
};
</script>

<template>
  <div class="flex-across tab-header-widget">
    <TabItemWidget
      v-for="(item, index) in props.items"
      :key="item.key"
      :item="item"
      :selected="index === selected"
      @selected="handleSelected"
    />
  </div>
</template>

<style scoped>
.tab-header-widget {
  margin: 6px 0;
}
</style>
