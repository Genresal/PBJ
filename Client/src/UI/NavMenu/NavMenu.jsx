import {Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText} from "@mui/material";
import classes from "./NavMenu.module.css"
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import HomeOutlinedIcon from '@mui/icons-material/HomeOutlined';
import AccountBoxOutlinedIcon from '@mui/icons-material/AccountBoxOutlined';
import PeopleAltOutlinedIcon from '@mui/icons-material/PeopleAltOutlined';

const NavMenu = () => {

    return (
        <div>
            <Drawer variant="permanent" anchor="left" className={classes.drawer}>
                <List>
                    <ListItem>
                        <ListItemButton>
                            <ListItemIcon>
                                <HomeOutlinedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Home"/>
                        </ListItemButton>
                    </ListItem>
                    <ListItem>
                        <ListItemButton>
                            <ListItemIcon>
                                <SearchOutlinedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Explore"/>
                        </ListItemButton>
                    </ListItem>
                    <ListItem>
                        <ListItemButton>
                            <ListItemIcon>
                                <PeopleAltOutlinedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Followers"/>
                        </ListItemButton>
                    </ListItem>
                    <ListItem>
                        <ListItemButton>
                            <ListItemIcon>
                                <HomeOutlinedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Followings"/>
                        </ListItemButton>
                    </ListItem>
                    <ListItem>
                        <ListItemButton>
                            <ListItemIcon>
                                <AccountBoxOutlinedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Profile"/>
                        </ListItemButton>
                    </ListItem>
                </List>
            </Drawer>
        </div>

    );
};

export default NavMenu;