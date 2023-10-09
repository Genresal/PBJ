import React, { useEffect, useState } from 'react'
import { useFetching } from '../../../../hooks/useFetching';
import {getFollowersAsync} from "../../api/getFollowersAsync";
import { getFollowingsAsync } from '../../api/getFollowingsAsync';
import { Grid, CircularProgress } from '@mui/material';
import FollowerCard from '../FollowerCard/FollowerCard';

export default function Followers({userEmail, isFollowers}) {
  const [followers, setFollowers] = useState([]);
  const [page, setPage] = useState(1);
  const [take, setTake] = useState(10);


  const [initializeFollowers, isLoading] = useFetching(async () => {
    let response;

    if (isFollowers) {
      response = await getFollowersAsync(userEmail, page, take);
    }
    else {
      response = await getFollowingsAsync(userEmail, page, take);
    }

    setFollowers([...followers, ...response.data.items]);
  })

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
              <FollowerCard key={follower.followerEmail} follower={follower}/>
            )
      }
    </Grid>
  )
}
