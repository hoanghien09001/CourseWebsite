<template>
  <v-app>
    <NavBar/>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>
import NavBar from "./NavBar";

export default {
  components: {
    NavBar,
  },
};
</script>