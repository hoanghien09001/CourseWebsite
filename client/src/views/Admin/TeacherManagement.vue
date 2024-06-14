<template>
  <div class="container mt-5">
    <h3 class="text-h4">Xét duyệt chứng chỉ</h3>

    <v-container class="mt-10">
      <v-row>
        <v-col cols="3">
          <v-select
            label="Các loại chứng chỉ đã tồn tại"
            :items="certType"
          ></v-select>
        </v-col>
        <v-col cols="4">
          <v-form @submit.prevent>
            <div class="d-flex">
              <v-text-field label="Thêm loại chứng chỉ" required></v-text-field>
              <v-btn class="mt-2 ms-2" type="submit">Thêm</v-btn>
            </div>
          </v-form>
        </v-col>
      </v-row>
    </v-container>

    <p class="text-h6">Cấp quyền giảng viên</p>
    <v-container class="mt-8">
      <v-expansion-panels>
        <v-expansion-panel v-for="user in users" :key="user">
          <v-expansion-panel-title>
            <v-row>
              <v-col cols="1" class="text-center mt-5"
                ><v-avatar> <v-img :src="user.avatar" /> </v-avatar
              ></v-col>

              <v-col cols="3">
                <p class="text-subtitle-1 font-weight-bold mt-2">
                  {{ user.fullname }}
                </p>
                <p class="">{{ user.email }}</p>
              </v-col>

              <v-col cols="2" class="mt-6">
                <v-chip color="red">{{ user.certificate.name }}</v-chip>
              </v-col>
              <v-col cols="3">
                <p class="text-subtitle-2 font-weight-bold">Mô tả</p>
                <p>{{ user.certificate.description }}</p>
              </v-col>
              <v-col cols="3">
                <div>
                  <img
                    class="p-1"
                    style="border: 1px dashed red"
                    width="150"
                    :src="user.certificate.image"
                    alt=""
                  />
                </div>
              </v-col>
            </v-row>
          </v-expansion-panel-title>
          <v-expansion-panel-text>
            <v-btn>Cấp quyền</v-btn>
            <v-btn>Khóa tài khoản</v-btn>
            <v-btn>
              <v-icon icon="delete_forever" color="red"></v-icon>
            </v-btn>
          </v-expansion-panel-text>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-container>
  </div>
</template>

<script>
import certificateService from "../../services/certificate/certificate.services.js";

export default {
  data() {
    return {
      users: [],
      certType: [],
    };
  },
  mounted() {
    certificateService.getAllCertificateType().then((res) => {
      console.log(res);
      this.certType = res.data.map((cert) => cert.name);
    });

    certificateService.getUserHaveCertificate().then((res) => {
      this.users = res.data;
      console.log(this.users);
    });
  },
};
</script>
