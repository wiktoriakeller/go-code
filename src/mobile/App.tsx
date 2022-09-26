import { SignInPage } from "./pages/SignInPage";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { RootStackParamList } from "./navigation/stackNavigation";
import { SignUpPage } from "./pages/SignUpPage";
import { createDrawerNavigator } from "@react-navigation/drawer";
import { RootDrawerParamList } from "./navigation/drawerNavigation";
import UserCourses from "./pages/UserCourses";
import AllCourses from "./pages/AllCourses";

const HomePage = () => {
  const Drawer = createDrawerNavigator<RootDrawerParamList>();

  return (
    <Drawer.Navigator>
      <Drawer.Screen 
        name="UserCourses"
        component={UserCourses}
      />
      <Drawer.Screen 
        name="AllCourses"
        component={AllCourses}
      />
    </Drawer.Navigator>
  )
}

export default function App() {
  const Stack = createNativeStackNavigator<RootStackParamList>();

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="SignIn">
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
