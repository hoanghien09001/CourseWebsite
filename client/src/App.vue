<template>
  <the-header style="z-index: 199"></the-header>
  <router-view></router-view>
  <the-footer></the-footer>
</template>

<script>
import TheHeader from "./components/TheHeader.vue";
import TheFooter from "./components/TheFooter.vue";

export default {
  components: {
    "the-header": TheHeader,
    "the-footer": TheFooter,
  },
};
</script>

<style>
#app {
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

nav {
  padding: 30px;
}
</style>
