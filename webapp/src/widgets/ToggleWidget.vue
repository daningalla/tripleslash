<script setup lang="ts">
import { faToggleOff, faToggleOn } from "@fortawesome/pro-light-svg-icons";
import { computed, defineProps } from "vue";

const props = defineProps<{ label: string; modelValue: boolean }>();
const emit = defineEmits(["update:modelValue"]);
const icon = computed(() => (props.modelValue ? faToggleOn : faToggleOff));
</script>

<template>
  <div
    class="flex-across align-items-center cursor-pointer"
    @click="emit('update:modelValue', !modelValue)"
  >
    <div class="flex-across label">{{ label }}</div>
    <font-awesome-icon class="icon" :icon="icon" />
  </div>
</template>

<style scoped>
.label {
  margin-right: 12px;
}
.icon {
  font-size: 1.25em;
  padding-top: 2px;
}
</style>
