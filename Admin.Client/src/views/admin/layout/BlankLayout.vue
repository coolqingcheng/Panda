<template>
    <h1>test</h1>
    <KeepAlive :include="tabStore.getKeepAliveComponentNames" :exclude="tabStore.exCludeList">
        <RouterView></RouterView>
    </KeepAlive>
</template>
<script setup lang="ts">
import { useTabsViewStore } from '@/store/TabsViewStore';

const tabStore = useTabsViewStore();
</script>