# 🎮 SURVIVAL 300 Seconds (Top-Down Shooter)
> **"300초 동안 좁혀오는 포위망 속에서 살아남으세요."** > 유니티 엔진을 활용한 뱀서라이크 스타일의 탑다운 액션 게임 프로젝트입니다.

---

## 🚀 Playable Build & Documentation
* **📥 [게임 실행 파일 다운로드 (Google Drive)]**(https://drive.google.com/file/d/1MMJFK829wWIYJX9eZoxO0Two9-5FisGK/view?usp=sharing)
* **📄 [상세 기획 및 로직 설명서 (PDF)](./김두회(kdh22481834@gmaill.com).pdf)**

---

## ✨ 핵심 재미 요소 (Key Features)
* **난이도 설계:** 남은 시간 200초 기점으로 '파괴 불가능한 포탑(Turret)'이 등장하여 긴장감을 고조시킵니다, 100초에는 공격 주기 강화
* **압축된 피드백:** 5분(300초)이라는 명확한 제한 시간 내에 빠른 성취감을 느낄 수 있도록 설계되었습니다.

## 🛠 Tech Stack
* **Engine:** Unity
* **사용AI:** Gemini CLI
* **Language:** C#
* **Graphics:** 3D 오브젝트를 탑다운으로 보는 방식
* **Version Control:** Git / GitHub Desktop

## 🧠 주요 구현 기술 (Technical Highlights)
### 1. 시스템 아키텍처 및 최적화
* **Singleton Pattern:** `GameManager`를 싱글톤으로 설계하여 게임의 전체 상태(Timer, Spawn, GameState)를 전역에서 안정적으로 관리하고 데이터 무결성을 유지 (싱글톤:한명의 관리자)
* **메모리 최적화:** 모든 투사체와 소환된 오브젝트에 Destroy를 적용하여 메모리 누수 방지

### 2. 정교한 물리 및 수학적 연산
* **Raycasting 조준 시스템:** 마우스의 2D 스크린 좌표를 3D 월드 좌표로 변환
* **Vector & Quaternion:** `FromToRotation` 및 `LookAt` 함수를 사용하여 오브젝트의 방향성 계산 및 투사체 설정
* **Rigidbody 넉백효과:** `AddForce`를 이용한 넉백(Knockback) 시스템을 구현

### 3. 비동기 로직 및 AI 설계
* **Coroutine:** 피격 시 깜빡임 효과(`BlinkEffect`) 및 상태 복구 로직을 `Coroutine:실행 중단했다 중단한 시점에서 다시 시작`으로 처리하여 메인 루프의 성능 저하 없이 자연스러운 비동기 연출
* **Monster:** 추격형(Chaser)과 고정 사격형(Turret)

---

## 👤 Contact & Links
* **Developer:** 김두회 (Kim Du Hoe)
* **Email:** kdh22481834@gmail.com
* **GitHub:** [github.com/KimDuHoe](https://github.com/KimDuHoe)
