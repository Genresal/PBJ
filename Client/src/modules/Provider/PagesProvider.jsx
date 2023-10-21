import { UserManager, WebStorageStateStore } from 'oidc-client';
import { useState, createContext, useEffect } from 'react'
import axios from 'axios';
import { decodeToken } from 'react-jwt';
import { BirthDateClaim, EmailClaim, NameClaim, SurnameClaim } from '../../constants/ClaimConstants';

export const PagesContext = createContext(null);

const userManager = new UserManager({
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    includeIdTokenInSilentRenew: false,
    loadUserInfo: true,
    response_mode: "query",
    authority: "https://localhost:7069",
    client_id: "pbj-client",
    response_type: "code",
    scope: "openid profile smsAPI offline_access IdentityServerApi",
    redirect_uri: "http://localhost:3000/callback",
    silent_redirect_uri: "http://localhost:3000/refresh"
});

userManager.events.addAccessTokenExpired(async () => {
    await userManager.signinSilent();
    userManager.getUser().then(user => console.log(user.access_token))
});

export default function PagesProvider({children}) {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loggedUser, setLoggedUser] = useState({
        email: "",
        userName: "",
        surname: "",
        birthDate: "",
    });

    const ContextValues = {
        isAuthenticated,
        setIsAuthenticated,
        userManager,
        loggedUser,
        setLoggedUser
    }

    const getUser = (access_token) => {
    
        const decodedToken = decodeToken(access_token);

        console.log(decodedToken);

        const decodedUser = {
            email: decodedToken[EmailClaim],
            userName: decodedToken[NameClaim],
            surname: decodedToken[SurnameClaim],
            birthDate: decodedToken[BirthDateClaim],
        }

        return decodedUser;
    }

    useEffect(() => {
        userManager.getUser().then(userInfo => {
            if(userInfo) {
                setIsAuthenticated(true)
                
                axios.defaults.headers.common["Authorization"] = `Bearer ${userInfo.access_token}`;

                setLoggedUser(getUser(userInfo.access_token));
            }
        })
    }, [])

    return (
        <PagesContext.Provider value={ContextValues}>
            {
                children
            }
        </PagesContext.Provider>
    )
}
