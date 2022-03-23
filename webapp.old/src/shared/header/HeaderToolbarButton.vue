<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { defineComponent } from "vue";

defineProps(["icon", "label"]);
defineComponent("font-awesome-icon", FontAwesomeIcon);
</script>

<template>
    <div class="flex-column justify-space-between align-items-center component">
        <font-awesome-icon class="pad-top" :icon="icon" />
        <div class="pad-bottom font-sm">{{ label }}</div>
    </div>
</template>

<style scoped>
.component {     
    cursor: default;
    width: 76px;
    height: 76px;
    transition-duration: var(--hover-transition);
}
.component:hover {
    background-color: var(--theme-background-header-hover);
}
.pad-top {
    padding-top: 20px;
}
.pad-bottom {
    padding-bottom: 12px;
}
</style>