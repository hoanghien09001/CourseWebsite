<template>
    <div class="container ">
      <v-virtual-scroll :items="listCourse" :item-height="itemHeight" height="500">
        <template v-slot:default="{ item, index }">
          <div class="row mt-5">
            <div class="col-10" :key="index">
              <div class="course d-flex">
                <div class="image" style="width: 350px; height: 100%">
                  <v-img
                    :width="250"
                    :height="150"
                    aspect-ratio="16/9"
                    cover
                    :src="item.imageCourse"
                  ></v-img>
                </div>
                <div class="content ms-5 d-flex flex-column">
                  <strong>
                    <p class="font-weight-bold text-h5">{{ item.courseName }}</p>
                    <p>{{ "Giá: " + item.price + " VNĐ" }}</p>
                  </strong>
                  <div class="align-content-end">
                    <button class="btn btn-outline-danger">Xóa</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </template>
      </v-virtual-scroll>
    </div>
  </template>
  
  <script>
  import CourseService from "@/services/course/course.service";
  
  export default {
    data() {
      return {
        listCourse: [],
        itemHeight: 50, 
      };
    },
    async mounted() {
      var res = await CourseService.getAllCourseATeacher();
      this.listCourse = res.data.data;
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
  