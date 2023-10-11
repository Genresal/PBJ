import React, { useEffect, useState } from 'react'
import { useFetching } from '../../../../hooks/useFetching';
import {getFollowersAsync} from "../../api/getFollowersAsync";
import { getFollowingsAsync } from '../../api/getFollowingsAsync';
import { deleteUserFollowerAsync } from "../../api/deleteUserFollowerAsync"
import { createUserFollowerAsync } from "../../api/createUserFollowerAsync"
import { Grid, CircularProgress } from '@mui/material';
import FollowerCard from '../FollowerCard/FollowerCard';

export default function Followers({loggedUserEmail, isFollowers}) {
  const [followers, setFollowers] = useState([]);
  const [page, setPage] = useState(1);
  const [take, setTake] = useState(10);

  console.log(followers);


  const [initializeFollowers, isLoading] = useFetching(async () => {
    let response;

    if (isFollowers) {
      response = await getFollowersAsync(loggedUserEmail, page, take);
    }
    else {
      response = await getFollowingsAsync(loggedUserEmail, page, take);
    }

    setFollowers([...followers, ...response.data.items]);
  })

  const handleFollowersClick = async (followerEmail, isFollow) => {
    let response;

    if (isFollow) {
      response = await createUserFollowerAsync(followerEmail, loggedUserEmail);
    }
    else {
      response = await deleteUserFollowerAsync(followerEmail, loggedUserEmail);
    }

    if (response) {
      setFollowers(followers.map(x => 
      x.email === followerEmail
      ? {...x, email: x.email, isFollowingRequestUser: !x.isFollowingRequestUser}
      : x))
    }
  }
  
  useEffect(() => {
    initializeFollowers()
  }, [page, take])

  const handleLoadMore = () => {
    setPage(prev => prev++);
  }

  return (
    <Grid container maxWidth="md" style={{width: 500}}>
      {isLoading
        ?
          <Grid container style={{ display: 'flex', justifyContent: "center", marginTop: 50 }}>
            <CircularProgress />
          </Grid>
        :
          followers.map(follower => 
              <FollowerCard key={follower.email} follower={follower} 
              handleFollowersClick={handleFollowersClick}/>
            )
      }
    </Grid>
  )
}
