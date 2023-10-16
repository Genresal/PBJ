import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom/cjs/react-router-dom.min'
import { getUserAsync } from '../modules/User/api/getUserAsync';
import { useFetching } from '../hooks/useFetching';
import NavMenu from '../UI/NavMenu/NavMenu';
import { Loader } from '../UI/Loader/Loader';
import UserPageCard from '../modules/User/components/UserPageCard/UserPageCard';
import Posts from '../modules/Post/components/Posts/Posts';
import { Grid } from '@mui/material';
import { getUserFollowerAsync } from '../modules/User/api/getUserFollowerAsync';
import { useContext } from 'react';
import { PagesContext } from '../modules/Provider/PagesProvider';
import {createUserFollowerAsync} from "../modules/User/api/createUserFollowerAsync"
import {deleteUserFollowerAsync} from "../modules/User/api/deleteUserFollowerAsync"

export default function UserPage() {
    const {loggedUser} = useContext(PagesContext)
    const params = useParams();
    const [user, setUser] = useState({});
    const [isFollower, setIsFollower] = useState(true);

    const [initializeUser, isLoading] = useFetching(async () => {
        const response = await getUserAsync(params.email);

        if (response) {
            setUser(response.data);

            const followerResponse = await getUserFollowerAsync(response.data.email, loggedUser.email)

            if (!followerResponse) {
                setIsFollower(false)
            }
        }

        return user;
    })

    const handleFollowUnfollowClick = async (userEmail) => {

        if (!isFollower) {
            const response = await createUserFollowerAsync(userEmail, loggedUser.email);

            if(response.status === 200) {
                setIsFollower(true)
            }
        }
        else {
            const response = await deleteUserFollowerAsync(userEmail, loggedUser.email);

            if(response.status === 200) {
                setIsFollower(false)
            }
        }
    }

    useEffect(() => {
        initializeUser();
    }, [])

    return (
        <>
            <NavMenu/>

            {isLoading
                ?
                    <Loader/>
                :
                    <Grid container maxWidth="md" style={{width: 500}}>
                        <UserPageCard user={user} isFollower={isFollower} handleFollowUnfollowClick={handleFollowUnfollowClick}/>
                        <Posts user={user} isLoggedUser={false}/>
                    </Grid>
            }
        </>
    )
}
