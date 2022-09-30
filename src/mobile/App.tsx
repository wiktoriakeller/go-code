import 'react-native-gesture-handler';
import { View, Image, Animated } from 'react-native';
import { SignInPage } from "./pages/SignInPage";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { RootStackParamList } from "./navigation/stackNavigation";
import { SignUpPage } from "./pages/SignUpPage";
import { createDrawerNavigator, DrawerContentComponentProps, DrawerContentScrollView, DrawerItemList, useDrawerStatus } from "@react-navigation/drawer";
import { RootDrawerParamList } from "./navigation/drawerNavigation";
import { UserCoursesPage } from "./pages/UserCoursesPage";
import { AllCoursesPage } from "./pages/AllCoursesPage";
import colors from './styles/colors';
import { useEffect, useState } from 'react';

const Stack = createNativeStackNavigator<RootStackParamList>();
const Drawer = createDrawerNavigator<RootDrawerParamList>();

const DrawerContent = (props: DrawerContentComponentProps) => {
  //const isDrawerOpen = useDrawerStatus();
  const [fadeAnim, setFadeAnim] = useState(new Animated.Value(0));
  
  useEffect(() => {
    Animated.timing(
      fadeAnim,
      {
        toValue: 1,
        duration: 2000,
        useNativeDriver: false
      }
    ).start();
  }, [fadeAnim]);

  useEffect(() => {
    setFadeAnim(new Animated.Value(0));
  }, [props.state]);

  return(
    <DrawerContentScrollView>
      <Animated.View 
        style={{
          flex: 1,
          flexDirection: "row",
          alignItems: "center",
          justifyContent: "center",
          marginBottom: 10,
          marginTop: 5,
          opacity: fadeAnim
        }}
      >
        <Image
          source={require("./assets/splash.png")}
        />
      </Animated.View >
      <DrawerItemList {...props}/>
    </DrawerContentScrollView>
  )
}

const HomePage = () => {
  return (
    <Drawer.Navigator 
      initialRouteName="UserCourses"
      drawerContent={(props) => <DrawerContent {...props} />}
      >
      <Drawer.Screen 
        name="UserCourses"
        component={UserCoursesPage}
        options={{ 
          title: "My courses",
          drawerActiveBackgroundColor: colors.primaryLight,
          drawerActiveTintColor: colors.white,
        }}
      />
      <Drawer.Screen 
        name="AllCourses"
        component={AllCoursesPage}
        options={{ 
          title: "All courses",
          drawerActiveBackgroundColor: colors.primaryLight,
          drawerActiveTintColor: colors.white
        }}
      />
    </Drawer.Navigator>
  )
}

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator 
        initialRouteName="SignIn">
        <Stack.Screen 
          name="SignIn" 
          component={SignInPage}
          options={{ title: "Sign In"}}
        />
        <Stack.Screen 
          name="SignUp" 
          component={SignUpPage} 
          options={{ title: "Sign Up"}}
        />
        <Stack.Screen 
          name="Home" 
          component={HomePage} 
          options={{ headerShown: false }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
