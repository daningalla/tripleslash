<script setup>
import SmallFormatLogo from "../../branding/SmallFormatLogo.vue";
import HeaderToolbarButton from "./HeaderToolbarButton.vue";
import { faGear, faArrowRightToBracket } from "@fortawesome/pro-light-svg-icons";

const testClick = (e) => {
    serviceIndex
        .getResource()
        .subscribe((value) => {
            console.log(value);
        });
}
</script>

<template>
    <div class="flex justify-space-between component">
        <div class="flex align-items-center">
            <SmallFormatLogo />
        </div>        
        <div class="flex">
            <HeaderToolbarButton :icon="faGear" label="Settings" />
            <HeaderToolbarButton :icon="faArrowRightToBracket" label="Sign In" />
        </div>
    </div>
</template>

<style scoped>
.component {
    background-color: var(--theme-background-header);
    height: 76px;
}
</style>