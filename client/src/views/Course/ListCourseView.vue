<template>
  <div class="container mt-12">
    <h3 class="font-weight-bold">Khóa học</h3>
    <hr />
    <div class="text-center" v-if="listCourse.length == 0">
      <p>Chưa có khóa học nào...</p>
      <p>
        Sau khi bạn xuất bản khóa học, hãy truy cập vào đây để tìm hiểu về mức
        độ tương tác với khóa học của bạn.
      </p>

      <v-btn variant="outlined" class="text-h6 mb-16" :to="'/create-course'"
        >Chuyển đến Bảng tạo khóa học</v-btn
      >
    </div>

    <div class="row mt-10">
      <div class="col-5" v-for="(course, i) in listCourse" :key="i">
        <div class="course d-flex">
          <div class="image" style="width: 250px; height: 100%">
            <img
              :src="course.imageCourse"
              style="max-width: 100%; max-height: 100%"
              alt=""
            />
          </div>
          <div class="content ms-5 d-flex flex-column">
            <strong>
              <p class="font-weight-bold text-h5">{{ course.courseName }}</p>
              <p>{{ "Giá: " + course.price + " VNĐ" }}</p>
            </strong>
            <div class="align-content-end">
              <router-link
                class="btn btn-outline-success me-3"
                :to="{
                  name: 'EditCoursePage',
                  params: { id: course.courseId },
                }"
              >
                Chỉnh sửa
              </router-link>
              <button class="btn btn-outline-danger">Xóa</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SubjectService from "@/services/course/subject.service";
import CourseService from "@/services/course/course.service";

export default {
  data() {
    return {
      listCourse: [],
    };
  },
  async mounted() {
    // await CourseService.getAllCourseATeacher()
    //   .then((res) => {
    //     this.listCourse = res.data.data;
    //     console.log("listCourse: "+this.listCourse);
    //   })
    //   .catch((err) => {
    //     console.log(err);
    //   });
    var res=await CourseService.getAllCourseATeacher();
    this.listCourse=res.data.data;
  },
};
</script>

<style scoped>
.container {
  height: 500px;
}

.course {
  border: 2px solid gray;
}

.btn {
  border-radius: 0;
}
</style>
