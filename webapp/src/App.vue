<script setup lang="ts">
import { themeService } from "@/services/theme";
import { onMounted } from "vue";
import LayoutRoot from "@/layout/LayoutRoot.vue";

onMounted(() => themeService.initialize());
</script>

<template>
  <LayoutRoot />
</template>

<style>
@import "./assets/layout.css";
@import "assets/styles.css";
</style>
