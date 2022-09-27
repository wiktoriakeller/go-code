import { DrawerScreenProps } from "@react-navigation/drawer";

type RootDrawerParamList = {
  UserCourses: undefined;
  AllCourses: undefined;
};

type HomeDrawerNavigation = DrawerScreenProps<RootDrawerParamList, "UserCourses">;
type AllCoursesDrawerNavigation = DrawerScreenProps<RootDrawerParamList, "AllCourses">;

export { RootDrawerParamList, HomeDrawerNavigation, AllCoursesDrawerNavigation };
