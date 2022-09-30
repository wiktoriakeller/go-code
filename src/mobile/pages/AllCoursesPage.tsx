import { View } from "react-native"
import React, { useEffect, useState } from "react"
import { CourseListItem } from "../components/courses/CourseListItem"
import { getAllCoursesInfos, IGetAllCoursesInfosResponse, ICourseInfo } from "../api/courses/getAllCoursesInfos";
import { IApiResponse } from "../api/common";
import Spinner from "react-native-loading-spinner-overlay/lib";
import { ISignUpForCourseResponse, signUpForCourse } from "../api/courses/signUpForCourse";
import { useIsFocused } from '@react-navigation/native';
import { FlatList } from "react-native-gesture-handler";
import colors from "../styles/colors";

interface IRegisterCourse {
  courseId: number;
  registered: boolean;
}

export const AllCoursesPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [courses, setCourses] = useState<ICourseInfo[]>([]);
  const [reload, setReload] = useState(true);
  const isFocused = useIsFocused();

  useEffect(() => {
    setIsLoading(true);
    getAllCoursesInfos()
    .then((response: IApiResponse<IGetAllCoursesInfosResponse>) => setCourses(response.data?.courses as ICourseInfo[]))
    .catch((error: IApiResponse<IGetAllCoursesInfosResponse>) => console.log(error))
    .finally(() => setIsLoading(false))
  }, [reload, isFocused]);

  const registerForCourseButton = (props: IRegisterCourse) => {
    return {
      text: "Sign up",
      isDisabled: props.registered,
      onPress: () => {
        setIsLoading(true);
        signUpForCourse({
          id: props.courseId
        })
        .then(() => console.log("registered"))
        .catch((error: IApiResponse<ISignUpForCourseResponse>) => console.log(error))
        .finally(() => {
          setIsLoading(false);
          setReload(!reload);
        })
      }
    }
  };

  return (
    <View style={{ backgroundColor: colors.background }}>
      <Spinner
        visible={isLoading}
        textContent={""}
      />
      <View style={{ marginBottom: 6 }}/>
      <FlatList
        data={courses}
        renderItem={({item}) => (
          <CourseListItem
            course={item}
            button={registerForCourseButton({ courseId: item.id, registered: item.isUserSignedUp })}
          />
        )}
        keyExtractor={course => course.id.toString()}
      />
      <View style={{ marginTop: 6 }}/>
    </View>
  )
}
