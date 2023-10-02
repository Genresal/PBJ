import CallbackPage from "../../pages/CallbackPage";
import HomePage from "../../pages/HomePage";
import StartPage from "../../pages/StartPage";

export const privateRoutes = [
    { path: "/", component: HomePage, exact: true },
]

export const publicRoutes = [
    { path: "/", component: StartPage, exact: true },
    { path: "/callback", component: CallbackPage, exact: true}
]