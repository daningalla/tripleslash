<script setup lang="ts">
import type { IconDefinition } from "@fortawesome/pro-thin-svg-icons";

defineProps<{
  icon: IconDefinition;
  label: string;
}>();
</script>

<template class="widget">
  <div class="flex flex-column flex-align-items-center component">
    <div class="icon-wrap">
      <font-awesome-icon class="icon" :icon="icon" />
    </div>
    <div>
      <span class="label">{{ label }}</span>
    </div>
  </div>
</template>

<style scoped>
.component {
  width: 76px;
  height: 100%;
  font-size: var(--font-sz-icon-sm);
  transition: var(--hover-transition);
  cursor: pointer;
  color: var(--theme-foreground-subtle);
}
.component:hover {
  background-color: var(--theme-background-header-hover);
  color: var(--theme-foreground-header-hover);
}
.icon-wrap {
  padding-top: 16px;
}
.label {
  font-size: var(--font-sz-sm);
}
</style>
