<template>
  <div class="container-fluid">
    <h4 class="font-weight-bold">Tổng quan khóa học</h4>
    <v-divider></v-divider>
    <p class="my-12">
      Trang tổng quan khóa học của bạn rất quan trọng đối với thành công của bạn
      trên Coursery. Nếu được thực hiện đúng, trang này cũng có thể giúp bạn
      hiển thị trong các công cụ tìm kiếm như Google. Khi bạn hoàn thành phần
      này, hãy nghĩ đến việc tạo Trang tổng quan khóa học hấp dẫn thể hiện lý do
      ai đó muốn ghi danh khóa học của bạn
    </p>
    <v-form @submit.prevent="addCourse">
      <v-container>
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="course.courseName"
              label="Tên khóa học"
              variant="outlined"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field
              v-model="course.introduce"
              label="Giới thiệu"
              variant="outlined"
              required
            ></v-text-field>
          </v-col>

          <v-col cols="2">
            <v-text-field
              v-model="course.price"
              label="Mức giá"
              variant="outlined"
              suffix="VNĐ"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="2">
            <v-text-field
              v-model="course.code"
              label="Mã"
              variant="outlined"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="4">
            <v-file-input
              v-model="course.image"
              accept="image/*"
              label="Chọn hình ảnh khóa học"
              prepend-icon="add_a_photo"
              id="imgInput"
              @change="onFileChange"
              variant="outlined"
              required
            ></v-file-input>
          </v-col>

          <v-col>
            <div class="preview mb-5">
              <img
                :src="url"
                id="blah"
                style="max-width: 100%; max-height: 100%"
                alt=""
              />
              <span v-if="!url">
                <v-icon icon="image"></v-icon>
                <strong> Hình ảnh khóa học </strong>
              </span>
            </div>
          </v-col>
        </v-row>

        <v-btn
          type="submit"
          class="text-subtitle-1"
          variant="flat"
          color="#5865f2"
          >TẠO KHÓA HỌC</v-btn
        >
      </v-container>
    </v-form>
  </div>
</template>

<script>
import courseService from "@/services/course/course.service";

export default {
  data() {
    return {
      course: {},
      url: "",
      user: {},
    };
  },
  computed: {
    getUser() {
      return this.$store.state.auth.user;
    },
  },
  methods: {
    onFileChange(e) {
      const file = e.target.files[0];
      this.url = URL.createObjectURL(file);
    },
    addCourse() {
      courseService
        .createCourse(this.course, this.getUser.Id)
        .then((res) => {
          this.$toast.success(
            "Tạo khóa học mới thành công. Hãy tiếp tục thêm các môn học mới vào khóa học của bạn"
          );
          this.course = {};
          this.url = "";
        })
        .catch((err) => {
          this.$toast.error("Tạo khóa học thất bại");
        });
    },
  },
};
</script>

<style>
.preview {
  border: 1px dashed green;
  width: 300px;
  height: 250px;
  overflow: hidden;
  display: inline-block;
  text-align: right;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
