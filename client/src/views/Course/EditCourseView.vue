<template>
  <div style="min-height: 800px">
    <div class="container mt-10">
      <div class="advertise-course d-flex py-10 px-8">
        <div class="content">
          <h3 class="font-weight-bold">Khóa học: {{ course.courseName }}</h3>
          <p class="text-subtitle-1">{{ course.introduce }}</p>
        </div>

        <div class="image">
          <img :src="course.imageCourse" alt="" />
        </div>
      </div>
      <div class="alert" v-if="listSubject.length == 0">
        <p>Chưa có môn học nào...</p>
        <p>Hãy tạo mới một số môn học</p>
      </div>

      <div class="row">
        <div class="col-8">
          <div class="my-10">
            <strong class="text-h5">Nội dung khóa học:</strong>
          </div>
          <div>
            <v-expansion-panels class="mb-6" variant="accordion">
              <v-expansion-panel v-for="subject in listSubject" :key="subject">
                <v-expansion-panel-title>
                  {{ subject.subjectName }}
                </v-expansion-panel-title>
                <v-expansion-panel-text>
                  <div class="pa-4">
                    <div class="row">
                      <div
                        class="col-12 subject-detail"
                        v-for="(subjectDetail,sbIndex) in subject.listSubjectDetail"
                        :key="sbIndex"
                      >
                        <p class="text-subtitle-2">
                          <v-icon icon="ondemand_video"></v-icon>
                          <span class="ms-3"
                            >Bài
                            {{ subjectDetail.subjectDetailId + ": " }}</span
                          >
                          {{ subjectDetail.subjectDetailName }}
                        </p>

                        <span>{{}}</span>
                      </div>
                    </div>
                    <div class="button-add-subjectdetail text-right">
                      <v-dialog v-model="dialog" max-width="600">
                        <template v-slot:activator="{ props: activatorProps }">
                          <v-btn
                            class="text-none font-weight-regular"
                            text="Thêm bài giảng mới"
                            variant="tonal"
                            v-bind="activatorProps"
                          ></v-btn>
                        </template>

                        <v-card>
                          <v-form
                            @submit.prevent="
                              addSubjectDetail(subject.subjectId)
                            "
                          >
                            <v-card-text>
                              <v-row dense>
                                <v-col cols="12">
                                  <v-text-field
                                    label="Tên bài giảng"
                                    variant="outlined"
                                    v-model="subjectDetail.subjectDetailName"
                                    required
                                  ></v-text-field>
                                  <v-text-field
                                    label="Link video"
                                    variant="outlined"
                                    v-model="subjectDetail.linkVideo"
                                    required
                                  ></v-text-field>
                                </v-col>
                              </v-row>
                            </v-card-text>

                            <v-card-actions>
                              <v-spacer></v-spacer>

                              <v-btn
                                color="primary"
                                text="Tạo"
                                type="submit"
                                class="mb-3 me-3"
                                variant="tonal"
                              ></v-btn>
                            </v-card-actions>
                          </v-form>
                        </v-card>
                      </v-dialog>
                    </div>
                  </div>
                </v-expansion-panel-text>
              </v-expansion-panel>
            </v-expansion-panels>
          </div>
        </div>
        <div class="col-4">
          <div class="mt-8 ms-8">
            <v-text-field
              placeholder="Tên môn học"
              variant="outlined"
              v-model="subject.subjectName"
              width="300px"
            ></v-text-field>
            <button
              class="btn btn-outline-info"
              type="button"
              @click="addSubject"
            >
              Tạo môn học mới
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import courseService from "@/services/course/course.service";
import subjectService from "@/services/course/subject.service";
import getBlobDuration from "get-blob-duration";

export default {
  data() {
    return {
      course: {},
      dialog: false,
      subject: {},
      listSubject: [],
      subjectDetail: {},
    };
  },
  mounted() {
    courseService
      .getAllCourseATeacher()
      .then((res) => {
        this.course = res.data.data.find(
          (x) => x.courseId == this.$route.params.id
        );
        // console.log("course ne: ", this.course);
        // console.log("data ne: ", res.data.data);
        // console.log("id ne: ", this.$route.params.id);
      })
      .catch((err) => {
        console.log(err);
      });

    subjectService
      .getAllSubjectByCourseId(this.$route.params.id)
      .then((res) => {
        this.listSubject = res.data;

        for (let i = 0; i < this.listSubject.length; i++) {
          console.log("ahihi");
          subjectService
            .getAllSubjectDetail(this.listSubject[i].subjectId)
            .then((res) => {
              this.listSubject[i].listSubjectDetail = res.data;
              console.log("list subject: ", this.listSubject);

              //get list subject-detail mỗi subject:
            })
            .catch((err) => {
              console.log("lỗi: ", err);
            });
        }
      })
      .catch((err) => {
        console.log("ahihi: ", err);
      });
  },
  methods: {
    addSubject() {
      this.subject.symbol = this.course.code;
      subjectService
        .createSubjectToCourse(this.course.courseId, this.subject)
        .then((res) => {
          this.$toast.success("Tạo môn học mới thành công");
          this.subject = {};
          subjectService
            .getAllSubjectByCourseId(this.$route.params.id)
            .then((res) => {
              this.listSubject = res.data;
              // console.log("list subject: ", this.listSubject);
            })
            .catch((err) => {
              console.log("ahihi: ", err);
            });
        })
        .catch((err) => {
          this.$toast.error("Tạo môn học mới thất bại");
        });
    },
    addSubjectDetail(subjectId) {
      this.subjectDetail.subjectId = subjectId;
      subjectService
        .addSubjectDetail(this.subjectDetail)
        .then((res) => {
          this.$toast.success("Thêm bài giảng thành công");
          this.dialog = false;
        })
        .catch((err) => {
          this.$toast.error("Thêm bài giảng mới thất bại");
        });
    },
  },
};
</script>

<style>
.alert {
  border-radius: 10px;
  border: 2px solid orange;
  display: inline-block;
}

.advertise-course {
  background-color: rgb(232, 226, 226);
}

.advertise-course img {
  border-radius: 18px;
}

.subject-detail {
  cursor: pointer;
  text-decoration: underline;
}
</style>
