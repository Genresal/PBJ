import NavMenu from '../UI/NavMenu/NavMenu'
import { useContext, useState } from 'react';
import { PagesContext } from '../modules/Provider/PagesProvider';
import UserProfile from '../modules/User/components/UserProfile/UserProfile';

export default function ProfilePage() {
    const {user, setUser} = useContext(PagesContext);

    return (
        <>
            <NavMenu/>

            <UserProfile user={user} saveUser={setUser}/>
        </>
    )
}
