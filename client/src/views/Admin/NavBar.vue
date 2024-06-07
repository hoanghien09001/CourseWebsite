<template>
  <nav>
    <v-app-bar class="bg-deep-purple-lighten-5">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-app-bar-title class="font-weight-bold">
        Coursery <span class="text-red-lighten-2"> Admin </span>
        <v-icon icon="security" color="red-lighten-2" />
      </v-app-bar-title>
      <v-spacer></v-spacer>
      <v-btn>
        <span class="pe-3" @click="logout()"> Đăng xuất </span>
        <v-icon icon="logout"></v-icon>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer temporary v-model="drawer">
      <v-list>
        <v-list-item class="text-center mt-5">
          <v-avatar size="100">
            <v-img
              src="https://res.cloudinary.com/dbfevlfy3/image/upload/v1716452444/xyz-abc_638520744418520563image.jpg"
            ></v-img>
          </v-avatar>
          <p class="text-subtitle-1 font-weight-bold mt-2 text-center">
            <v-chip color="green">
              <v-icon icon="local_police" />
              <span class="font-weight-bold ms-2"> Admin </span>
            </v-chip>
          </p>
        </v-list-item>
        <v-divider></v-divider>

        <v-list-item
          v-for="(item, i) in links"
          :key="i"
          :value="item"
          color="primary"
          :to="item.route"
          class="button-group"
        >
          <strong>
            <v-list-item-title
              class="button d-flex justify-content-center"
              v-text="item.text"
            ></v-list-item-title>
          </strong>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>

<script>
import authServices from "../../services/auth.services";

export default {
  data() {
    return {
      drawer: false,
      links: [
        {
          text: "Xét duyệt chứng chỉ",
          route: "/teacher-management",
        },
        {
          text: "Quản lý học viên",
          route: "/student-management",
        },
      ],
    };
  },
  methods: {
    logout() {
      authServices.logout();
      this.$store.dispatch("auth/logout");
      this.$router.push("/");
    },
  },
};
</script>

<style scoped>
.button-group {
  /* background-color: white; */
  margin: 0 10px;
  border: 1px solid rgb(220, 215, 215);
}

.v-list-item__content {
  color: black;
}

.v-list-item {
  margin-bottom: 5px;
  text-align: center;
}

.v-navigation-drawer {
  padding: 0;
}
</style>
