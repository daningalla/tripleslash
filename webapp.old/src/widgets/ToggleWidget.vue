<script setup>
import { computed, defineProps, ref } from "vue";
import { faToggleOff, faToggleOn } from "@fortawesome/pro-light-svg-icons";

const props = defineProps({
    label: {
        required: true,
        type: String,
    },
    modelValue: {
        required: false,
        default: false,
        type: Boolean
    },
});
const emit = defineEmits(["update:modelValue"]);
const icon = computed(() => { return props.modelValue ? faToggleOn : faToggleOff; });
</script>

<template> 
    <div class="flex component no-break align-items-center align-content-center"
            @click="emit('update:modelValue', !modelValue)">
        <div class="label">{{ label }}</div>
        <span class="widget-wrap">
            <font-awesome-icon class="fa" :icon="icon" />
        </span>
    </div>
</template>

<style>
.component { cursor: pointer; }
.label { font-size: var(--font-sz-normal); }
.widget-wrap { font-size: 12pt;  }
.fa { margin-left: 8px;  }
</style>