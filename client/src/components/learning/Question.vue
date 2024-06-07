<template>
  <div class="ma-16">
    <div>                                                                   <!--search-->
      <v-card class="ma-5" color="surface-light" max-width="700">
        <v-card-text>
          <v-text-field
            :loading="loading"
            append-inner-icon="mdi-magnify"
            density="compact"
            label="Search templates"
            variant="solo"
            hide-details
            single-line
            @click:append-inner="onClick"
          ></v-text-field>
        </v-card-text>
      </v-card>
    </div>
    <div>                                                                   <!--Bộ lọc-->
      <div class="d-flex mx-auto ma-5">
        <div class="w-25">
          <v-select
            label="Bộ lọc"
            :items="['Tất cả các bài giảng', 'Bài giảng hiện tại']"
            variant="solo"
            max-width="200"
          ></v-select>
        </div>
        <div class="w-75">
          <v-select
            label="Sắp xếp theo"
            :items="[
              'Sắp xếp theo thứ tự đề xuất',
              'Sắp xếp theo thứ tự gần đây nhất',
              'Sắp xếp theo thứ tự có nhiều người tán thành nhất',
            ]"
            variant="solo"
            max-width="500"
          ></v-select>
        </div>
      </div>
      <div>
        <v-select
          label="Select"
          :items="[
            'Câu hỏi tôi đang theo dõi',
            'Câu hỏi tôi đã hỏi',
            'Câu hỏi không có câu trả lời',
          ]"
          multiple
          max-width="300"
        ></v-select>
      </div>
    </div>
    <div>                                                                   <!--Câu hỏi-->
        <div>
            <span class="text-h6 font-weight-bold">Tất cả các câu hỏi trong khóa học này </span>
        </div>
        <div class="d-flex"
        v-for="(question, questionIndex) in lstQuestion"
        :key="questionIndex"
        >
          <div class="ma-2">                                                <!--avatar-->
            <v-avatar color="surface-variant" size="30"
                ><img :src="question.avatar" style="width: inherit;" alt=""
              /></v-avatar>
          </div>
          <div class="w-75">                                                             <!--content-->
            <div class="text-body-1 font-weight-bold ma-2">{{question.userName}}</div>
            <div class="text-body-2 ma-2">{{ question.question }}</div>
            <div class="d-flex">
              <div class="text-caption ma-2">{{question.subjectDetailName}}</div>
              <div class="text-caption ma-2">{{question.createTime}}</div>
            </div>
          </div>
          <div>
            <div class="d-flex">
              <div class="text-body-1 font-weight-bold ma-2">{{ question.numberOfAnswer }}</div>
              <router-link :to="{path: `answer/${question.id}`}"><font-awesome-icon icon="comment" class="ma-2" style="color: gray;"/></router-link>
            </div>
          </div>
        </div>
    </div>
    <div>
      <router-link :to="{path: `make-question/${selectedId}`}" class="text-body-1 font-weight-bold ma-2">Đặt câu hỏi mới</router-link>
    </div>
  </div>
</template>
<script>
import {studentApi} from "@/services/studentApi"
import {userApi} from "@/services/userApi"
export default {
  props: ['selectedId'],
  data: () => ({
    studentApi: studentApi(),
    userApi:userApi(),
    loaded: false,
    loading: false,
    lstQuestion:{},

  }),
  async mounted() {
    const courseId = this.$route.params.courseId;
    console.log(courseId);
    var res=await this.studentApi.GetAllQuestionByCourseId(courseId);
    console.log(res);
    this.lstQuestion=res.data;
    console.log(this.lstQuestion);
  },
  methods: {
    onClick() {
      this.loading = true;

      setTimeout(() => {
        this.loading = false;
        this.loaded = true;
      }, 2000);
    },
  },
};
</script>