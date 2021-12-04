<script setup lang="ts"></script>
<template>
  <router-view></router-view>
</template>

<style>
body {
  background: #f1f1f1;
}

p {
  padding: 0;
  margin: 0;
}

html,
body,
#app {
  padding: 0;
  margin: 0;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
}

#app {
  font-family: "Helvetica Neue", Helvetica, "PingFang SC", "Hiragino Sans GB",
  "Microsoft YaHei", "微软雅黑", Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.tool-bar {
  margin-bottom: 1rem;
  margin-top: 1rem;
}

a {
  text-decoration: none;
}

@keyframes test1 {
  0% {
    opacity: 0.1;
    /* transform: translateY(50px); */
  }
  100% {
    /* transform: translateY(0); */
    opacity: 1;
  }
}

/* .anim1 {
  animation: test1 0.3s ease-in-out;
} */
</style>
