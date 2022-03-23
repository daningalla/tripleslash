<script setup>
import RootLayout from "./shared/RootLayout.vue";
import { useTheme } from "./composables/theme.js";
import { defineComponent } from "vue";

useTheme();
</script>

<template>
  <RootLayout></RootLayout>
</template>

<style>
@import "./assets/css/theme.css";
@import "./assets/css/style.css";
@import "./assets/css/layout.css";
</style>
