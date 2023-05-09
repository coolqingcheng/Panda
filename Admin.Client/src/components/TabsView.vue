<template>
    <div class="v-tabs">
        <ul>
            <li v-for="item in 10" :key="item">菜单:{{ item }}</li>
        </ul>
    </div>
</template>
<script setup lang="ts">

</script>

<style lang="scss" scoped>
.v-tabs {
    width: 100%;
    box-sizing: border-box;
    height: 40px;
    background-color: var(--el-bg-color);
    border-bottom: 1px solid var(--el-border-color-light);
    border-top: 1px solid var(--el-border-color-light);

    ul {
        padding: 0;
        margin: 0;
        height: 100%;
        display: flex;
        flex-direction: row;
        list-style: none;
        align-items: center;
        li {
            padding-left: 8px;
            padding-right: 8px;
        }
    }
}
</style>