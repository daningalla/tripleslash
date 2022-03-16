<script setup lang="ts">
import SmallFormatLogo from "@/branding/SmallFormatLogo.vue";
import SettingsWidget from "@/shared/header/SettingsWidget.vue";
import ProfileWidget from "@/shared/header/ProfileWidget.vue";
</script>

<template>
  <div ref="headerRef" class="flex flex-space-between layout-header-component">
    <div>
      <SmallFormatLogo class="logo" />
    </div>
    <div class="flex">
      <SettingsWidget />
      <ProfileWidget />
    </div>
  </div>
</template>

<style scoped>
.layout-header-component {
  height: 76px;
  width: 100%;
  background-color: var(--theme-background-header);
}

.logo {
  width: 76px;
  height: 100%;
}

.layout-header-widget {
  width: 76px;
  display: flex;
  flex-direction: column;
}
</style>
