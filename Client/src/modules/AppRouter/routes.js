import CallbackPage from "../../pages/CallbackPage";
import FollowersPage from "../../pages/FollowersPage";
import HomePage from "../../pages/HomePage";
import PostPage from "../../pages/PostPage";
import ProfilePage from "../../pages/ProfilePage";
import RefreshPage from "../../pages/RefreshPage";
import StartPage from "../../pages/StartPage";
import UserPage from "../../pages/UserPage";

export const privateRoutes = [
    { path: "/", component: HomePage, exact: true },
    { path: "/refresh", component: RefreshPage, exact: true },
    { path: "/profile", component: ProfilePage, exact: true },
    { path: "/followers", component: FollowersPage, exact: true },
    { path: "/followings", component: FollowersPage, exact: true },
    { path: "/post/:userEmail/:postId", component: PostPage, exact: false },
    { path: "/:email", component: UserPage, exact: false }
]

export const publicRoutes = [
    { path: "/", component: StartPage, exact: true },
    { path: "/callback", component: CallbackPage, exact: true}
]