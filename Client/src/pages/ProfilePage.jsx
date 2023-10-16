import NavMenu from '../UI/NavMenu/NavMenu'
import { useContext, useState } from 'react';
import { PagesContext } from '../modules/Provider/PagesProvider';
import UserProfile from '../modules/User/components/UserProfile/UserProfile';

export default function ProfilePage() {
    const {loggedUser, setLoggedUser} = useContext(PagesContext);

    return (
        <>
            <NavMenu/>

            <UserProfile loggedUser={loggedUser} saveLoggedUserUser={setLoggedUser}/>
        </>
    )
}
