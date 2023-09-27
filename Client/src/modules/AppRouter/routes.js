import CallbackPage from "../../pages/CallbackPage";
import HomePage from "../../pages/HomePage";
import StartPage from "../../pages/StartPage";
import TestPage from "../../pages/TestPage";

export const privateRoutes = [
    { path: "/", component: HomePage, exact: true },
    { path: "/test", component: TestPage, exact: true }
]

export const publicRoutes = [
    { path: "/", component: StartPage, exact: true },
    { path: "/callback", component: CallbackPage, exact: true}
]