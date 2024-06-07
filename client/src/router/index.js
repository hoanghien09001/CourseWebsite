import { createWebHistory, createRouter } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import TeachingRegisterView from '@/views/TeachingRegisterView.vue'
import EditUserView from '@/views/EditUserView.vue'
import TheProfile from '@/components/users/TheProfile.vue'
import TheAlbum from '@/components/users/TheAlbum.vue'
import ThePassword from '@/components/users/ThePassword.vue'
import NotFound from '@/views/NotFound.vue'
import ConfirmEmailView from '@/views/ConfirmEmailView.vue'
import ForgotPasswordView from '@/views/ForgotPasswordView.vue'
import ConfirmCreateNewPasswordView from '@/views/ConfirmCreateNewPasswordView.vue'
import CartView from '@/views/CartView.vue'
import LearningView from '@/views/LearningView.vue'
import Overview from '@/components/learning/Overview.vue'
import Question from '../components/learning/Question.vue'
import Answer from '../components/learning/Answer.vue'
import MakeQuestion from '../components/learning/MakeQuestion.vue'
import BlogView from '@/views/Blog/BlogView.vue'
import CreateBlogView from '@/views/Blog/CreateBlogView.vue'
import SinglePost from '@/views/Blog/SinglePost.vue'
import AdminPage from '@/views/Admin/AdminPage.vue'
import TeacherManagement from '@/views/Admin/TeacherManagement.vue';
import StudentManagement from '@/views/Admin/StudentManagement.vue';
import CourseManagementView from '@/views/Course/CourseManagementView.vue';
import CreateCourseView from '@/views/Course/CreateCourseView.vue';
import ListCourseView from '@/views/Course/ListCourseView.vue';
import EditCourseView from '@/views/Course/EditCourseView.vue'


const routes = [
    {
        path: '/',
        name: 'HomePage',
        component: HomeView
    },
    {
        path: '/login',
        name: 'LoginPage',
        component: LoginView
    },
    {
        path: '/register',
        name: 'RegisterPage',
        component: RegisterView
    },
    {
        path: '/teaching',
        name: 'TeachingRegisterPage',
        component: TeachingRegisterView
    },
    {
        path: '/user/',
        name: 'EditUserPage',
        component: EditUserView,
        children: [
            {
                path: 'edit/profile',
                name: 'EditProfile',
                component: TheProfile
            },
            {
                path: 'edit/album',
                name: 'EditAlbum',
                component: TheAlbum
            },
            {
                path: 'edit/password',
                name: 'ChangePassword',
                component: ThePassword
            }

        ]
    },
    {
        path: '/confirm-email',
        name: "ConfirmEmail",
        component: ConfirmEmailView
    },
    {
        path: '/forgot-password',
        name: "ForgotPassword",
        component: ForgotPasswordView
    },
    {
        path: '/confirm-new-password',
        name: "ConfirmNewPassword",
        component: ConfirmCreateNewPasswordView
    },
    {
        path: "/cart",
        Name: "ShoppingCart",
        component: CartView
    },
    {
        path: "/learning/:courseId",
        Name: "LearningView",
        component: LearningView,
        children: [
            {
                path: "overview",
                name: 'Overview',
                component: Overview
            },
            {
                path: "question",
                name: 'QuestionView',
                component: Question
            },
            {
                path: "answer/:questionId",
                name: 'Answer',
                component: Answer
            },
            {
                path: "make-question/:subjectDetailId",
                name: 'MakeQuestion',
                component: MakeQuestion
            },

        ]
    },
    {
        path: '/:catchAll(.*)',
        name: 'NotFound',
        component: NotFound
    },
    {
        path: '/blog',
        name: "BlogPage",
        component: BlogView
    },
    {
        path: '/blog/publish',
        name: "CreateBlogPage",
        component: CreateBlogView
    },
    {
        path: '/post',
        name: 'SinglePost',
        component: SinglePost
    },
    {
        path: '/admin',
        name: 'AdminPage',
        component: AdminPage,
        beforeEnter: (to, from, next) => {
            let role = JSON.parse(localStorage.getItem('user')).Permission
            console.log('test: ', role);
            console.log(role.includes("Admin"));
            if (role.includes("Admin") ) {
                console.log('true hihi');
                next();
            } else {
                console.log('false huhu');
                next({ path: '/', replace: true });
            }
        },
        children: [
            {
                path: '/teacher-management',
                name: 'TeacherManagement',
                component: TeacherManagement
            },
            {
                path: '/student-management',
                name: 'StudentManagement',
                component: StudentManagement
            },
        ]
    },
    {
        path: '/course-mangament',
        name: 'CourseManagementPage',
        component: CourseManagementView,
        children: [
            {
                path: '/create-course',
                name: 'CreateCoursePage',
                component: CreateCourseView
            },
            {
                path: '/list-course',
                name: 'ListCoursePage',
                component: ListCourseView
            },
            {
                path: '/edit-course/:id',
                name: 'EditCoursePage',
                component: EditCourseView
            },
        ]
    }
    
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router;