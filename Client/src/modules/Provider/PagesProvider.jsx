import { UserManager, WebStorageStateStore } from 'oidc-client';
import { useState, createContext, useEffect } from 'react'
import axios from 'axios';
import { decodeToken } from 'react-jwt';
import { BirthDateClaim, EmailClaim, NameClaim, RoleClaim, SurnameClaim } from '../../constants/ClaimConstants';
import NavMenu from '../../UI/NavMenu/NavMenu';

export const PagesContext = createContext(null);

const userManager = new UserManager({
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    loadUserInfo: true,
    response_mode: "query",
    authority: "https://localhost:7069",
    client_id: "pbj-client",
    response_type: "code",
    scope: "openid profile smsAPI",
    redirect_uri: "http://localhost:3000/callback",
});

export default function PagesProvider({children}) {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState({
        email: "",
        name: "",
        surname: "",
        birthDate: "",
        role: ""
    });

    const ContextValues = {
        isAuthenticated,
        setIsAuthenticated,
        userManager,
        user,
        setUser
    }

    const getUser = (access_token) => {
    
        const decodedToken = decodeToken(access_token);

        console.log(decodedToken)

        const decodedUser = {
            email: decodedToken[EmailClaim],
            name: decodedToken[NameClaim],
            surname: decodedToken[SurnameClaim],
            birthDate: decodedToken[BirthDateClaim],
            role: decodedToken[RoleClaim]
        }

        return decodedUser;
    }

    useEffect(() => {
        userManager.getUser().then(async (userInfo) => {
            if(userInfo) {
                setIsAuthenticated(true)
                
                axios.defaults.headers.common["Authorization"] = `Bearer ${userInfo.access_token}`;

                setUser(getUser(userInfo.access_token));
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
