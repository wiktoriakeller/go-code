import SignInPage from "./pages/SignInPage";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { RootStackParamList } from "./navigation/navigationTypes";
import SignUpPage from "./pages/SignUpPage";

export default function App() {
  const Stack = createNativeStackNavigator<RootStackParamList>();

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="SignIn">
        <Stack.Screen 
          name="SignIn" 
          component={SignInPage} 
        />
        <Stack.Screen 
          name="SignUp" 
          component={SignUpPage} 
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
