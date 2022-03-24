<script setup lang="ts">
import {
  faCircleChevronLeft,
  faCircleChevronRight,
} from "@fortawesome/pro-duotone-svg-icons";

interface Props {
  page: number;
  hasNext: boolean;
}
const props = defineProps<Props>();
const emit = defineEmits(["prevClicked", "nextClicked"]);
</script>

<template>
  <div class="cursor-pointer flex-across">
    <div v-if="props.page > 0" class="flex-across align-items-center" @click="emit('prevClicked')">
      <font-awesome-icon :icon="faCircleChevronLeft" />
      <div class="previous">Previous</div>
    </div>
    <div class="bar" v-if="props.page > 0 && props.hasNext">|</div>
    <div v-if="props.hasNext" class="flex-across align-items-center" @click="emit('nextClicked')">
      <div class="next">Next</div>
      <font-awesome-icon :icon="faCircleChevronRight" />
    </div>
  </div>
</template>

<style scoped>
.bar {
  margin: 0 8px;
}
.next {
  margin-right: 8px;
}
</style>
