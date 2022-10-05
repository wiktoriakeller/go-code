import { View } from "react-native"
import React, { useEffect, useState } from "react"
import { CourseListItem } from "../components/courses/CourseListItem"
import { IGetAllCoursesInfosResponse, getAllCoursesRequest } from "../api/courses/getAllCoursesInfos";
import Spinner from "react-native-loading-spinner-overlay/lib";
import { ISignUpForCourseRequest, ISignUpForCourseResponse, signUpForCourseRequest } from "../api/courses/signUpForCourse";
import { useIsFocused } from '@react-navigation/native';
import { FlatList } from "react-native-gesture-handler";
import { useQuery } from "../api/useQuery";
import colors from "../styles/colors";

interface IRegisterCourse {
  courseId: number;
  registered: boolean;
}

export const AllCoursesPage = () => {
  const [reload, setReload] = useState(true);
  const isFocused = useIsFocused();
  
  const { 
    data: courses, 
    fetchData: getAllCoursesInfos, 
    isLoading: coursesLoading, 
    isSuccess: coursesSuccess 
  } = useQuery<any, IGetAllCoursesInfosResponse>();

  const { 
    fetchData: signUpForCourse, 
    isLoading: signUpLoading,
    isSuccess: signUpSuccess
  } = useQuery<ISignUpForCourseRequest, ISignUpForCourseResponse>();

  useEffect(() => {
    getAllCoursesInfos(getAllCoursesRequest());
  }, [reload, isFocused]);

  useEffect(() => {
    if(signUpSuccess) {
      setReload(!reload);
    }
  }, [signUpSuccess]);

  const registerForCourseButton = (props: IRegisterCourse) => {
    return {
      text: "Sign up",
      isDisabled: props.registered,
      onPress: () => {
        signUpForCourse(signUpForCourseRequest({ id: props.courseId }));
      }
    }
  };

  return (
    <View style={{ backgroundColor: colors.background }}>
      <Spinner
        visible={coursesLoading || signUpLoading}
        textContent={""}
      />
      <View style={{ marginBottom: 6 }}/>
      <FlatList
        data={coursesSuccess ? courses?.courses : []}
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
