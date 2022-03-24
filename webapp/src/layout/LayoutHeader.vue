<script setup lang="ts">
import logo from "@/branding/logo-76x76.png";
import HeaderButton from "@/layout/HeaderButton.vue";
import { faGear, faArrowRightToBracket } from "@fortawesome/pro-light-svg-icons";
</script>

<template>
  <div class="flex-across header justify-space-between">
    <div>
      <img :src="logo" alt="logo" />
    </div>
    <div class="flex-across">
      <HeaderButton label="Settings" :icon="faGear" />
      <HeaderButton label="Sign In" :icon="faArrowRightToBracket" />
    </div>
  </div>
</template>

<style scoped>
.header {
  background-color: var(--ts-background-header);
  height: 76px;
  position: fixed;
  width: 100%;
}
</style>
