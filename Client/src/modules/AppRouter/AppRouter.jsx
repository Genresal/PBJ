import { Switch, Route, Redirect } from "react-router-dom";
import { publicRoutes, privateRoutes } from "./routes";
import { useContext } from "react";
import { PagesContext } from "../Provider/PagesProvider";

export default function AppRouter() {
    const {isAuthenticated} = useContext(PagesContext)

    return (
        <Switch>
           {(isAuthenticated ? privateRoutes : publicRoutes).map(route =>
                <Route
                    key={route.path}
                    component={route.component}
                    path={route.path}
                    exact={route.exact}
                />
            )}
            <Redirect to="/" />
        </Switch>
    )
}
