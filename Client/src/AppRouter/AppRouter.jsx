import { Switch, Route } from "react-router-dom";
import { publicRoutes, privateRoutes } from "../router";
import { useContext } from "react";
import { PagesContext } from "../PagesProvider";

export default function AppRouter() {
    const {isAuthenticated} = useContext(PagesContext)

    console.log(isAuthenticated)

    return (
        isAuthenticated
        ?
            <Switch>
                {privateRoutes.map(route => 
                    <Route
                        key={route.path}
                        component={route.component}
                        path={route.path}
                        exact={route.exact}
                    />
                )}
            </Switch>
        :
            <Switch>
                {publicRoutes.map(route => 
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
