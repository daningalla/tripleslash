<script setup lang="ts">
import type { SearchResultGroup } from "@/dto/search";
import PackageItem from "@/views/search/PackageItem.vue";

defineProps<{ itemGroup: SearchResultGroup }>();
</script>

<template>
  <div class="flex-down">
    <PackageItem
      class="item"
      v-for="(item, index) in itemGroup.results"
      :key="index"
      :item="item"
    />
  </div>
</template>

<style scoped>
.item {
  margin: 16px 0;
}
</style>
