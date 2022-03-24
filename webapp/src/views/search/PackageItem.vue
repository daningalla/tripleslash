<script setup lang="ts">
import type { SearchResultItem } from "@/dto/search";
import NugetLogo from "@/assets/icons/NugetLogo.vue";
import { faExclamationCircle } from "@fortawesome/pro-duotone-svg-icons";
import { isPreRelease } from "@/core/semver";
import { computed } from "vue";
import vImgFallback from "@/directives/img-fallback";
import sourceUrl from "@/assets/icons/NugetLogo";

const props = defineProps<{ item: SearchResultItem }>();
const tags = computed(() =>
  props.item.tags && props.item.tags.length > 0
    ? props.item.tags.join(", ")
    : undefined
);
</script>

<template>
  <div class="flex-across">
    <div class="package-icon">
      <NugetLogo v-if="!item.iconUrl" />
      <img
        v-if="item.iconUrl"
        v-img-fallback="sourceUrl"
        :src="item.iconUrl"
        alt="package-icon"
      />
    </div>
    <div
      class="flex-down margin-left-32 no-wrap overflow-hidden text-overflow-ellipses"
    >
      <div class="package-title font-lg">
        {{ item.packageId.id }}
      </div>
      <div class="package-detail flex-across align-items-center font-sm">
        <div class="no-wrap">Latest version: {{ item.packageId.version }}</div>
        <font-awesome-icon
          v-if="isPreRelease(item.packageId.version)"
          class="pre-release"
          :icon="faExclamationCircle"
        />
        <div
          class="margin-left-16 overflow-hidden text-overflow-ellipses"
          v-if="tags"
        >
          #{{ tags }}
        </div>
      </div>
      <div class="description">
        {{ item.description }}
      </div>
    </div>
  </div>
</template>

<style scoped>
.package-icon img,
.package-icon svg {
  width: 32px;
  height: 32px;
  padding-top: 3px;
}
.package-detail {
  margin-top: 5px;
  color: var(--ts-foreground-subtle);
}
.pre-release {
  margin-left: 5px;
  --fa-primary-color: white;
  --fa-secondary-color: gold;
}
.description {
  margin-top: 8px;
  white-space: normal;
}
</style>
