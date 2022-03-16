<script setup lang="ts">
import WidgetTemplate from "@/shared/header/WidgetTemplate.vue";
import { faGear } from "@fortawesome/pro-light-svg-icons";
const handleClick = () => {
  console.log("Clicked");
};
</script>

<template>
  <WidgetTemplate :icon="faGear" label="Settings" @click="handleClick"/>
</template>
