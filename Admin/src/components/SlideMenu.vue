<template>
    <h2 class>操作</h2>
    <router-link to="/dash/post/write">写一篇</router-link>
</template>

<script lang="ts">
import { defineComponent } from "vue";


export interface SildeMenuItem {

    title: string
    links: { url: string, name: string }[]
}

export default defineComponent({
    setup(){
        
    }
})
</script>

<style>
</style>