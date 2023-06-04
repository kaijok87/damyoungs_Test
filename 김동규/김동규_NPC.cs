
    몬스터 : Player가 던전에 입장시 무작위 위치에 몬스터가 소환된다.
        - 외계인(Alien) -Alien, Br_Robot, Space_Soldier, Big_Alien, EVA
        - 반란군(Rebel) - Soldier, Medic, Crew.Crew_Captain
        - 우주해적(Scavenger) - Hunter, Junker

----몬스터 액션
        (A)이동
           -A.몬스터가 Player를 향해 이동할 때 조건
                (1)시야범위 내 Player가 존재할 시
                    (1-1)Player사이에 장애물이 존재하지 않을때
                (2)1이상의 데미지를 Player에게 직접 받았을 때
                (3)이벤트가 발생했을 때

        (B)전투
           -B.몬스터가 Player를 공격할 때 조건
                (1)공격범위 내 Player가 존재할 시
                    (1-1)Player사이에 벽이 존재하지 않을때
                (2)근처 Player가 존재할 때 -> 공포게이지를 증가
                (3)HP가 0이하일때 주변 타일의 자폭을 시전

        (C)드랍
           -C.몬스터가 아이템 드랍하는 조건
                (1)HP가 0이하일때 아이템과 암흑에너지를 드랍
                (2)몬스터와 관련된 퀘스트를 진행줄 일 때



----몬스터 패턴
        (A)이동
           -A.이동
                (1)Player를 향해 N만큼 이동한다. -> 이동방향은 정면
                    (1-1)이동방향에 장애물이 존재할 경우 -> 정면 기준 좌측방향으로 이동
                (2)Random.Range %확률로 텔레포트 스킬을 시전
                    (2-1)N턴후 Player 주변으로 이동

        (B)전투
           -B.전투
                (1)Random.Range %확률로 근접 or 원거리을 선택
                    (근접)Random.Range %확률로 기본 or 스킬 선택
                        (기본)기본타입의 근접 공격을 한다.
                        (스킬)스킬타입의 근접 공격을 한다.
                    (원거리)Random.Range %확률로 기본 or 스킬 선택
                        (기본)기본타입의 원거리 공격을 한다.
                        (스킬)스킬타입의 원거리 공격을 한다.
                (2)Random.Range %확률로 스킬 시전
                    (텔레포트)N턴이후 Player주변으로 이동한다.
                    (쉴드)N턴동안 받는 데미지를 감소시킨다.
                    (디버프)근처 Player에게 상태이상을 적용한다. //독, 마비, 빙결, 수면, 화상, 감기, 슬로우


-----------------------------------------------------------------------------------------------------------------------------]

    NPC : 기본적으로 우주선 내부에 존재하며, 가끔씩 던전 내부에서도 모습을 보인다.
        - 외계인(Alien) - Alien, Br_Robot, Space_Soldier, Big_Alien, EVA
        - 정규군(Regular Army) - Soldier, Medic, Crew. Crew_Captain
        - 우주해적(Scavenger) - Hunter, Junker

----NPC 상호작용
        (A)대화
        (B)퀘스트
        (C)상점
        (D)동료/용병





























































