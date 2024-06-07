<template>
  <div class="d-flex">
    <div>
      <div >
        <iframe
          width="1000"
          height="560"
          :src="selectedSubjectDetail ? selectedSubjectDetail.linkVideo : 'https://www.youtube.com/embed/zoEtcR5EW08?si=fPLeZNCXYYIRgKmt'"
          title="YouTube video player"
          frameborder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
          referrerpolicy="strict-origin-when-cross-origin"
          allowfullscreen
        ></iframe>
      </div>
      <div>
        <v-card>
          <v-tabs bg-color="white" center-active>
            <v-tab>
              <router-link :to="{ name: 'Overview' }" class="text-span">
                <span>Tổng quan</span>
              </router-link>
            </v-tab>
            <v-tab>
              <router-link :to="{ name: 'QuestionView' }" class="text-span">
                <span>Hỏi đáp</span>
              </router-link>
            </v-tab>
            <v-tab>
              <router-link :to="{ name: 'QuestionView' }" class="text-span">
                <span>Ghi chú</span>
              </router-link>
            </v-tab>
            <v-tab>
              <router-link :to="{ name: 'QuestionView' }" class="text-span">
                <span>Thông báo</span>
              </router-link>
            </v-tab>
            <v-tab>
              <router-link :to="{ name: 'QuestionView' }" class="text-span">
                <span>Đánh giá</span>
              </router-link>
            </v-tab>
          </v-tabs>
        </v-card>
      </div>
      <div>
        <router-view :selectedId="selectedId"></router-view>
      </div>
    </div>
    <div style="width: 32%; height: 10rem">
      <div class="text-h6 font-weight-bold box-subject">Nội dung khóa học</div>
      <div>
        <v-expansion-panels>
          <v-expansion-panel
            v-for="(subject, subjectIndex) in listSubjects"
            :key="subjectIndex"
            :title="subject.subjectName"
          >
            <v-expansion-panel-text
            v-for="(subjectDetail, subjectDetailIndex) in listSubjectDetailFilter(subject.subjectName)"
            :key="subjectDetailIndex"
            >
              <button @click="selectedId=subjectDetail.subjectDetailId" style="width: 100%; text-align: justify;">
                <div>{{subjectDetail.subjectDetailName}}</div>
                <div class="d-flex">
                  <font-awesome-icon icon="tv" />
                  <div class="text-caption ml-2">6 phút</div>
                </div>
              </button>
            </v-expansion-panel-text>
          </v-expansion-panel>
        </v-expansion-panels>
      </div>
    </div>
  </div>
</template>

<script>
import { courseApi } from "@/services/courseApi";
export default {
  data() {
    return {
      courseApi: courseApi(),
      course: {},
      listSubjects: {},
      listSubjectDetails:{},
      lstDetail:[],
      selectedId:null
    };
  },
  async mounted() {
    const courseId = this.$route.params.courseId;
    try {
      const res = await this.courseApi.GetCourseById(courseId);
      this.course = res.data.data;
    } catch (e) {
      console.error("Error fetching course" + e.message);
    }
    console.log(courseId);
    const res1 = await this.courseApi.GetAllSubjectByCourseId(courseId);
    console.log(res1);
    this.listSubjects = res1.data;
    console.log(this.listSubjects);
    for(let i = 0; i < this.listSubjects.length; i++){
      const res2 = await this.courseApi.GetAllSubjectDetailBySubjectId(this.listSubjects[i].subjectId);
      this.listSubjectDetails[i]=res2.data;  
      console.log(i);
      console.log(this.listSubjectDetails[i]);
      for(let j = 0; j < this.listSubjectDetails[i].length; j++){
        this.lstDetail.push(this.listSubjectDetails[i][j]);
      }
    }
    console.log(this,this.lstDetail);
  },
  methods: {
    listSubjectDetailFilter(subjectName){
      return this.lstDetail.filter(x=>x.subjectName === subjectName)
    },
    
  },
  computed: {
    selectedSubjectDetail(){
      return this.lstDetail.find(x => x.subjectDetailId === this.selectedId)
    }
  }
};
</script>
<style>
.text-span {
  color: rgb(48, 47, 47);
  text-decoration: none;
  font-weight: bold;
  width: 100%;
  margin: 0;
  padding: 8px 0 8px 15px;
  text-align: start;
}
.box-subject {
  align-items: center;
  justify-content: space-between;
  padding: 0.8rem 0.8rem 0.8rem 1.6rem;
  border: 1px solid #d1d7dc;
  background-color: #fff;
}
</style>
