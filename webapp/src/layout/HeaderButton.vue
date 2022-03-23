<script setup lang="ts">
import type { IconDefinition } from "@fortawesome/fontawesome-svg-core";
defineProps<{ label: string, icon: IconDefinition }>();
</script>

<template>
  <div class="flex-down align-items-center component">
    <div class="icon"><font-awesome-icon :icon="icon" /></div>
    <div class="font-ex-small">{{ label }}</div>
  </div>
</template>

<style scoped>
.component {
  cursor: default;
  width: 76px;
  transition: background-color var(--ts-transition-delay) linear;
}
.component:hover {
  background-color: var(--ts-background-header-hover);
}
.icon {
  padding-top: 18px;
  padding-bottom: 8px;
}
</style>
