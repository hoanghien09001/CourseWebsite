<template>
  <v-card>
    <v-layout>
      <v-navigation-drawer expand-on-hover rail>
        <v-list density="compact" nav>
          <v-list-item
            v-for="(item, i) in links"
            :key="i"
            :value="item"
            :prepend-icon="item.icon"
            :to="item.route"
            class="my-3"
          >
            <v-list-item-title
              class="text-subtitle-2"
              v-text="item.text"
            ></v-list-item-title>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>

      <v-main> <router-view></router-view></v-main>
    </v-layout>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      links: [
        {
          text: "Tạo khóa học mới",
          route: "/create-course",
          icon: "add_circle",
        },
        {
          text: "Chỉnh sửa - Quản lý",
          route: "/list-course",
          icon: "edit_note",
        },
      ],
    };
  },
};
</script>

<style scoped>
.v-list-item__content {
  color: red;
}

.v-list-item {
  margin-bottom: 5px;
  font-size: large;
  /* text-align: center; */
}

.v-navigation-drawer {
  padding: 0;
}

.v-list-item--nav .v-list-item-title {
  font-weight: bolder !important;
}
</style>
