parallel 'Linux build': {
	try{
		stage('Linux build'){		
			node('Vodovoz') {
				def REFERENCE_ABSOLUTE_PATH = "${JENKINS_HOME}/workspace/Vodovoz_Vodovoz_master"

				echo 'Prepare Gtk.DataBindings'		
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions: 
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'Gtk.DataBindings']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/Gtk.DataBindings"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/Gtk.DataBindings.git']]
                ])

				echo 'Prepare GammaBinding'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'GammaBinding']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/GammaBinding"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/GammaBinding.git']]
                ])

				echo 'Prepare GMap.NET'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'GMap.NET']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/GMap.NET"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/GMap.NET.git']]
                ])
				
				echo 'Prepare My-FyiReporting'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/QSBuild']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'My-FyiReporting']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/My-FyiReporting"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/My-FyiReporting.git']]
                ])
                sh 'nuget restore My-FyiReporting/MajorsilenceReporting-Linux-GtkViewer.sln'
				
				echo 'Prepare QSProjects'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'QSProjects']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/QSProjects"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/QSProjects.git']]
                ])
                sh 'nuget restore QSProjects/QSProjectsLib.sln'
				
				echo 'Prepare Vodovoz'		
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: scm.branches,
                    doGenerateSubmoduleConfigurations: scm.doGenerateSubmoduleConfigurations,
                    extensions: scm.extensions 
                        + [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'Vodovoz']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/Vodovoz"]],
                    userRemoteConfigs: scm.userRemoteConfigs
                ])
                sh 'nuget restore Vodovoz/Vodovoz.sln'
				
				echo 'Build solution'
                sh 'msbuild /p:Configuration=DebugWin /p:Platform=x86 Vodovoz/Vodovoz.sln'
                fileOperations([fileDeleteOperation(excludes: '', includes: 'Vodovoz.zip')])
                zip zipFile: 'Vodovoz.zip', archive: false, dir: 'Vodovoz/Vodovoz/bin/DebugWin'
                archiveArtifacts artifacts: 'Vodovoz.zip', onlyIfSuccessful: true
			}
		}
	}
	catch (e) {
		echo "Ошибка в сборке на Linux. " + e
	}
	
}, 'Win build':{
	try{
		stage('Win build'){
            node('Vodovoz') {
                def REFERENCE_ABSOLUTE_PATH = "${JENKINS_HOME}/workspace/Vodovoz_Vodovoz_master"

                echo 'Prepare Gtk.DataBindings'		
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions: 
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'Gtk.DataBindings']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/Gtk.DataBindings"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/Gtk.DataBindings.git']]
                ])

                echo 'Prepare GammaBinding'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'GammaBinding']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/GammaBinding"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/GammaBinding.git']]
                ])

                echo 'Prepare GMap.NET'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'GMap.NET']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/GMap.NET"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/GMap.NET.git']]
                ])

                echo 'Prepare My-FyiReporting'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/QSBuild']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'My-FyiReporting']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/My-FyiReporting"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/My-FyiReporting.git']]
                ])
                sh 'nuget restore My-FyiReporting/MajorsilenceReporting-Linux-GtkViewer.sln'

                echo 'Prepare QSProjects'
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: [[name: '*/master']],
                    doGenerateSubmoduleConfigurations: false,
                    extensions:
                        [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'QSProjects']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/QSProjects"]],
                    userRemoteConfigs: [[url: 'https://github.com/QualitySolution/QSProjects.git']]
                ])
                sh 'nuget restore QSProjects/QSProjectsLib.sln'

                echo 'Prepare Vodovoz'		
                checkout changelog: false, poll: false, scm:([
                    $class: 'GitSCM',
                    branches: scm.branches,
                    doGenerateSubmoduleConfigurations: scm.doGenerateSubmoduleConfigurations,
                    extensions: scm.extensions 
                        + [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'Vodovoz']]
                        + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/Vodovoz"]],
                    userRemoteConfigs: scm.userRemoteConfigs
                ])
                sh 'nuget restore Vodovoz/Vodovoz.sln'

                echo 'Build solution'
                sh 'msbuild /p:Configuration=DebugWin /p:Platform=x86 Vodovoz/Vodovoz.sln'
                fileOperations([fileDeleteOperation(excludes: '', includes: 'Vodovoz.zip')])
                zip zipFile: 'Vodovoz.zip', archive: false, dir: 'Vodovoz/Vodovoz/bin/DebugWin'
                archiveArtifacts artifacts: 'Vodovoz.zip', onlyIfSuccessful: true
            }
		}
	}
	catch (e) {
		echo "Ошибка в сборке на Windows. " + e
	}	
}
node('Vod3') {
	stage('Deploy'){
		echo '${JOB_NAME}'
		echo "Checking the deployment for a branch " + env.BRANCH_NAME
		script{
			def OUTPUT_PATH = "F:\\WORK\\_BUILDS\\" + env.BRANCH_NAME
			if(
				env.BRANCH_NAME == 'master'
				|| env.BRANCH_NAME == 'develop'
				|| env.BRANCH_NAME == 'Beta'
				|| env.BRANCH_NAME ==~ /^[Rr]elease(.*?)/)
			{
				echo "Deploy branch " + env.BRANCH_NAME
				copyArtifacts(projectName: '${JOB_NAME}', selector: specific( buildNumber: '${BUILD_NUMBER}'));
				unzip zipFile: 'Vodovoz.zip', dir: OUTPUT_PATH
			}else{
				echo "Nothing to deploy"
			}
		}
	}
}
 
