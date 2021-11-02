<template>
    <div class="left-menu">
        <div class="menu">
            <slot name="menu"></slot>
        </div>
        <div class="content">
            <slot name="content"></slot>
        </div>
    </div>
</template>

<script>
export default {
    name: 'left-menu'
}
</script>

<style lang="scss" scoped>
.left-menu {
    display: flex;
    flex-direction: row;
    width: 100%;
}
.menu {
    min-width: 180px;
    border-right: 1px solid var(--el-border-color-light);
    display: flex;
    flex-direction: column;
    padding-right: 10px;
    box-sizing: border-box;
}
.content {
    display: flex;
    flex-direction: column;
    flex: 1;
    padding-left: 10px;
    box-sizing: border-box;
}
</style>