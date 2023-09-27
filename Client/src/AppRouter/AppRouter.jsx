import { Switch, Route } from "react-router-dom";
import { publicRoutes, privateRoutes } from "../router";
import { useContext } from "react";
import { PagesContext } from "../PagesProvider";

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
        </Switch>
    )
}
